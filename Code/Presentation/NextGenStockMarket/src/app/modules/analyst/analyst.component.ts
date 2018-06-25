import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { GameService } from '../service/game.service';
import { Chart } from 'angular-highcharts';

@Component({
  selector: 'app-analyst',
  templateUrl: './analyst.component.html',
  styleUrls: ['./analyst.component.css']
})
export class AnalystComponent implements OnInit {

  player: string;
  selectedSector: string;
  selectedStock: string;
  currentTurn: number = 0;

  isFormSubmitted = false;
  isDisplayGraph = false;

  graphData =[];
  bankInfo = [];
  sectors = [];
  stocks = [];
  
  constructor(private router: Router, private gameService: GameService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.player = params['player'];
      if (this.player != null || this.player != undefined) {
        this.getSectors();
      }
    });
  }

  chart = new Chart({
    chart: {
      type: 'line'
    },
    title: {
      text: 'Stock market variation'
    },
    credits: {
      enabled: false
    },
    series: [
      {
        name: 'Turns', 
        data: this.graphData
      }
    ]
  });

  // // add point to chart serie
  // add() {
  //   this.chart.addPoint(Math.floor(Math.random() * 10));
  // }

  getSectors() {
    this.gameService.getSectorData()
      .subscribe(sect => {
        this.sectors = sect;
      }, () => {
        this.toastr.warning("Error in loading Sectors", "Warning");
      });
  }

  getStocks() {
    this.gameService.getStockToSectors(this.selectedSector)
      .subscribe(stocks => {
        this.stocks = stocks;
      }, () => {
        this.toastr.warning("Error in loading stocks", "Warning");
      });
  }

  getCurrentTurn() {
    this.gameService.getBankData(this.player)
      .subscribe(bankdata => {
        this.currentTurn = bankdata.CurrentTurn;
      }, error => {
        this.toastr.warning(error, "Warning");
      });
  }

  generate() {
    this.isFormSubmitted = true;
 
    if(this.selectedSector == undefined || this.selectedStock == undefined) return;
    this.getCurrentTurn();
    this.gameService.getGraphData(this.selectedStock, this.selectedSector,this.currentTurn)
    .subscribe(res => {
        this.graphData.push(res);
        this.isDisplayGraph = true;
    }, error => {

    });
  }
}
