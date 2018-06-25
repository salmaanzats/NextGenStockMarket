import { GameService } from './../service/game.service';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { Stock } from '../model/stock';
import { BrokerService } from '../service/broker.service';
import { Constants } from '../core/constants';
import { BlockUiService } from '../core/services/block-ui.service';
import { GoogleStock } from '../model/googleStock';
import { YahooStock } from '../model/yahooStock';
import { AmazonStock } from '../model/amozonStock';
import { MicrosoftStock } from '../model/microsoftStock';


@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})

export class GameComponent implements OnInit {

  allStockData = [];
  google = new Array<GoogleStock>();
  yahoo = new Array<YahooStock>();
  amazon = new Array<AmazonStock>();
  microsoft = new Array<MicrosoftStock>();
  sectors = [];
  stocks = [];
  stockPrices = [];
  purchasedStock = [];

  stockEntity = new Stock();
  sellStockEntity = new Stock();

  selectedSector: string;
  selectedStock: string;
  selectedPurchasedStock: string;
  player: string;
  purchasedSelectedSector: string;
  purchasedSelectedStock: string;
  balance: number;
  stockPrice: number = 0;
  stockQuantity: number = 0;
  availableQuantity: number = 0;
  purchasedPrice: number = 0;
  totalAmount: number;
  currentTurn: number = 0;
  currentStockPrice: number = 0;
  income: number = 0;
  totalTurns: number = Constants.maximumTurn;
  isBlocked = false;
  isStockBought = true;
  isFormSubmitted = false;

  message = 'Please wait until all players connect';


  constructor(private router: Router, private gameService: GameService, private brokerService: BrokerService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.player = params['player'];
      if (this.player != null || this.player != undefined) {
        this.loadPlayerData();
        this.loadStockData();
        this.getCurrentTurn();
        this.gamePlay();
        this.getPurchasedStockData();
      }
    });
  }
  gamePlay() {
    this.gameService.getConnectedPlayers()
      .subscribe(playerCount => {
        if (playerCount.Result == Constants.maximumPlayers) {
          this.isBlocked = false;
        } else {
          this.isBlocked = true;
          this.gamePlay();
        }
      });
  }
  loadStockData() {
    this.gameService.getStockData()
      .subscribe(data => {
        this.allStockData = data;
        this.google = data[0].Sectors;
        this.yahoo = data[1].Sectors;
        this.microsoft = data[2].Sectors;
        this.amazon = data[3].Sectors;
        this.loadSectors();
      }, () => {
        this.toastr.warning("Error in loading stock data", "Warning");
      });
  }

  loadPlayerData() {
    this.gameService.getPlayerData(this.player)
      .subscribe(data => {
        this.balance = data.Accounts.Balance;
      }, error => {
        this.toastr.warning(error, "Warning");
        setTimeout(() => {
          this.router.navigate([''])
        }, 3000);
      })
  }

  loadSectors() {
    this.gameService.getSectorData()
      .subscribe(sect => {
        this.sectors = sect;
        this.getStocks();
      }, () => {
        this.toastr.warning("Error in loading Sectors", "Warning");
      });
  }

  getStocks() {
    this.gameService.getStockToSectors(this.selectedSector)
      .subscribe(stocks => {
        this.stocks = stocks;
        this.stockQuantity = null;
        this.stockPrice = null;
        this.totalAmount = null;
      }, () => {
        this.toastr.warning("Error in loading stocks", "Warning");
      });
  }

  loadPrice() {
    this.stockPrice = this.stocks.find(s => s.SectorName == this.selectedStock).StockPrice;
    this.stockPrice = Math.round(this.stockPrice * 100) / 100;
    this.stockQuantity = null;
    this.totalAmount = null;
  }

  calculateTotalAmount() {
    this.totalAmount = this.stockQuantity * this.stockPrice;
    this.totalAmount = Math.round(this.totalAmount * 100) / 100;
  }

  buyStocks() {
    this.isFormSubmitted = true;
    if (this.selectedSector == undefined || this.selectedStock == undefined || this.stockQuantity == null || this.stockPrice == null) return;
    this.stockEntity.PlayerName = this.player;
    this.stockEntity.Sector = this.selectedSector;
    this.stockEntity.Stock = this.selectedStock;
    this.stockEntity.Quantity = this.stockQuantity;
    this.stockEntity.StockPrice = this.stockPrice;
    this.stockEntity.Total = this.totalAmount;
    this.brokerService.buyStocks(this.stockEntity)
      .subscribe(() => {
        this.toastr.success("Your purchase has been successfully completed", "Success");
        this.loadPlayerData();
        this.getCurrentTurn();
        this.stockQuantity = 0;
        this.stockPrice = 0;
        this.totalAmount = 0;
        this.getPurchasedStockData();
        this.isStockBought = true;
        this.loadStockData();
      }, error => {
        this.toastr.warning(error, "Warning");
      });
  }

  gameOver() {
    this.gameService.getGameStatus()
      .subscribe(status => {
        if (status.Result == 'Game Over') {
          this.gameService.getWinner()
            .subscribe(winner => {
              this.isBlocked = true;
              this.message = 'winner :' + winner.Result.Accounts.PlayerName + '  Score:' + Math.round(winner.Result.Accounts.Balance);
              setTimeout(() => {
                this.gameService.newGame()
                  .subscribe(() => {
                    this.router.navigate(['']);
                  });
              }, 4000);
            });
        } else {
          this.isBlocked = true;
          this.message = 'Game Over...Please wait until other players finish';
          this.gameOver();
        }
      });
  }

  getCurrentTurn() {
    this.gameService.getBankData(this.player)
      .subscribe(bankdata => {
        this.currentTurn = bankdata.CurrentTurn;
        if (this.currentTurn == Constants.maximumTurn) {
          this.gameOver();
        }
      }, error => {
        this.toastr.warning(error, "Warning");
      });
  }

  getPurchasedStockData() {
    this.brokerService.getBrokerData(this.player)
      .subscribe(data => {
        if (data.BrokerInfos.length == 0) {
          this.isStockBought = false;
        }
        else {
          this.purchasedStock = data.BrokerInfos;
          this.purchasedStock = this.purchasedStock.filter(c => c.IsAvailable == true);
        }
      }, () => {
        this.toastr.warning("Error in loading Sectors", "Warning");
      });
  }

  getCurrentStockPrice() {
    this.brokerService.getBrokerData(this.player)
      .subscribe(() => {
        this.purchasedStock.forEach(element => {
          let s = element.Sector + " - " + element.Stock;

          this.purchasedSelectedSector = element.Sector;
          this.purchasedSelectedStock = element.Stock;

          if (s == this.selectedPurchasedStock) {
            this.availableQuantity = element.Quantity;
            this.purchasedPrice = Math.round(element.StockPrice * 100) / 100;
            let sectorStock = this.selectedPurchasedStock.split(" - ", 2);
            for (let market of this.allStockData) {
              if (market.StockMarket.CompanyName == sectorStock[0]) {
                market.Sectors.forEach(sector => {
                  if (sector.SectorName == sectorStock[1]) {
                    this.currentStockPrice = Math.round(sector.StockPrice * 100) / 100;
                    this.income = Math.round((this.currentStockPrice * this.availableQuantity) * 100) / 100;
                  }
                });
              }
            }
          }
        });
      }, () => {
        this.toastr.warning("Error in loading Sectors", "Warning");
      });
  }

  sellStocks() {
    this.isFormSubmitted = true;
    if(this.selectedPurchasedStock == undefined) return;
    this.sellStockEntity.PlayerName = this.player;
    this.sellStockEntity.Sector = this.purchasedSelectedSector;
    this.sellStockEntity.Stock = this.purchasedSelectedStock;
    this.sellStockEntity.Quantity = this.availableQuantity;
    this.sellStockEntity.StockPrice = this.currentStockPrice;
    this.sellStockEntity.Total = this.income;
    this.brokerService.SellStocks(this.sellStockEntity)
      .subscribe(res => {
        this.toastr.success("You have successfully sold selected sector!", "Success");
        this.loadPlayerData();
        this.getCurrentTurn();
        this.availableQuantity = 0;
        this.currentStockPrice = 0;
        this.income = 0;
        this.getPurchasedStockData();
        this.loadStockData();
      }, error => {
        this.toastr.error("Error in solding the selected stock", "Error");
      });
  }
}
