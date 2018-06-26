import { Component, OnInit, ViewContainerRef, ElementRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { GameService } from '../service/game.service';
import { chart } from 'highcharts';
import { Constants } from '../core/constants';
import { MaxStocks } from '../model/maxStock';
import { MinStocks } from '../model/minStocks';

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

  graphData = [];
  bankInfo = [];
  sectors = [];
  stocks = [];
  turns = [];
  maxStocks = new MaxStocks();
  minStocks = new MinStocks();

  minSectors:string;
  maxSectors:string;


  @ViewChild('chartTarget') chartTarget: ElementRef;
  chart: Highcharts.ChartObject;

  constructor(private router: Router, private gameService: GameService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.getTotalturns();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.player = params['player'];
      if (this.player != null || this.player != undefined) {
        this.getSectors();
        this.getSectorAnalyst();
        this.getStockAnalyst();
      }
    });
  }

  getTotalturns() {
    let i = 0;
    while (i == Constants.maximumTurn) {
      this.turns.push(i);
      i++;
    }
  }

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
    if (this.selectedSector == undefined || this.selectedStock == undefined) return;
    this.getCurrentTurn();
    this.gameService.getGraphData(this.selectedStock, this.selectedSector)
      .subscribe(res => {
        this.graphData = res;
        this.isDisplayGraph = true;
        this.getStockGraph();
      }, error => {
      });
  }

  getSectorAnalyst() {
    this.gameService.getStockAnalyst()
      .subscribe(res => {
        this.maxStocks = res.Max;
        this.minStocks = res.Min;
      })
  }

  getStockAnalyst() {
    this.gameService.getSectorAnalyst()
      .subscribe(res => {
        this.minSectors = res.Max.Sector;
        this.maxSectors =res.Min.Sector
      })
  }


  getStockGraph() {
    var x = this.graphData;
    const options: Highcharts.Options = {
      chart: {
        zoomType: 'x'
      },
      title: {
        text: ''
      },
      subtitle: {
        text: document.ontouchstart == null ?
          'Click and drag in the plot area to zoom in' : 'Pinch the chart to zoom in'
      },
      xAxis: {
        categories: this.turns
      },
      yAxis: {
        title: {
          type: 'double',
          stepSize: 1,
          min: 0,
          text: 'Stock Price'
        }
      },
      legend: {
        enabled: false
      },
      plotOptions: {
        area: {
          fillColor: {
            linearGradient: {
              x1: 0,
              y1: 0,
              x2: 0,
              y2: 1
            },
            stops: [
              [0, '#6c6efa'],
              [1, '#4648c0']

            ]
          },
          marker: {
            radius: 4
          },
          lineWidth: 1,
          states: {
            hover: {
              lineWidth: 1
            }
          },
          threshold: null
        }
      },
      series: [{
        type: 'area',
        name: 'Active',
        data: this.graphData,
      }]
    };
    this.chart = chart(this.chartTarget.nativeElement, options);
  }
}
