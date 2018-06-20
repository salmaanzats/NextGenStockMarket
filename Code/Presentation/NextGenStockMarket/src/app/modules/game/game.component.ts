import { GameService } from './../service/game.service';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { Stock } from '../model/stock';
import { BrokerService } from '../service/broker.service';
import { Constants } from '../core/constants';
import { BlockUiService } from '../core/services/block-ui.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})

export class GameComponent implements OnInit {

  google = [];
  yahoo = [];
  amazon = [];
  microsoft = [];
  sectors = [];
  stocks = [];
  stockPrices = [];

  stockEntity = new Stock();

  selectedSector: string;
  selectedStock: string;
  player: string;
  balance: number;
  stockPrice: number = 0;
  stockQuantity: number =0;
  totalAmount: number;
  currentTurn: number = 0;
  totalTurns: number = Constants.maximumTurn;
  isBlocked = false;
  message = 'Please wait until all players connect';
  isFormSubmitted = false;

  constructor(private router: Router, private gameService: GameService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef, private blockUiService: BlockUiService) {
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
      }
    });
  }
  gamePlay() {
    this.gameService.getConnectedPlayers()
      .subscribe(playerCount => {
        if (playerCount == Constants.maximumPlayers) {
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
        this.google = data[0];
        this.yahoo = data[1];
        this.microsoft = data[2];
        this.amazon = data[3];
        this.loadSectors();
      }, error => {
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
      }, error => {
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
      }, error => {
        this.toastr.warning("Error in loading stocks", "Warning");
      });
  }

  loadPrice() {
    this.stockPrice = this.stocks.find(s => s.SectorName == this.selectedStock).StockPrice;
    this.stockQuantity = null;
    this.totalAmount = null;
  }

  calculateTotalAmount() {
    this.totalAmount = this.stockQuantity * this.stockPrice;
  }

  buyStocks() {
    this.isFormSubmitted = true;
if(this.selectedSector == '' || this.selectedStock== ''|| this.stockQuantity== 0 ||this.stockPrice== 0)return;
    this.stockEntity.PlayerName = this.player;
    this.stockEntity.Sector = this.selectedSector;
    this.stockEntity.Stock = this.selectedStock;
    this.stockEntity.Quantity = this.stockQuantity;
    this.stockEntity.StockPrice = this.stockPrice;
    this.gameService.buyStocks(this.stockEntity)
      .subscribe(bought => {
        this.toastr.success("Your purchase has been successfully completed", "Success");
        this.loadPlayerData();
        this.getCurrentTurn();
        this.stockQuantity = null;
        this.stockPrice = null;
        this.totalAmount = null;
      }, error => {
        this.toastr.warning(error, "Warning");
      });
  }

  gameOver() {
    this.gameService.getGameStatus()
      .subscribe(status => {
        if (status == 'Game Over') {
          this.gameService.getWinner()
            .subscribe(winner => {
              this.isBlocked = true;
              this.message = 'winner :' + winner.Accounts.PlayerName + '  Score:' + winner.Accounts.Balance;
              setTimeout(() => {
                this.gameService.newGame()
                  .subscribe(res => {
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
}
