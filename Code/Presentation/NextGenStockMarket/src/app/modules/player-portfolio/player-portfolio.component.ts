import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { GameService } from '../service/game.service';
import { ToastsManager } from 'ng2-toastr/src/toast-manager';
import { BrokerService } from '../service/broker.service';

@Component({
  selector: 'app-player-portfolio',
  templateUrl: './player-portfolio.component.html',
  styleUrls: ['./player-portfolio.component.css']
})
export class PlayerPortfolioComponent implements OnInit {

  player: string;
  brokerInfo = [];

  constructor(private router: Router, private brokerService: BrokerService, private gameService: GameService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.player = params['player'];
      if (this.player != null || this.player != undefined) {
        this.loadPlayerPortfolio();
      }
    });
  }

  loadPlayerPortfolio() {
    this.brokerService.getBrokerData(this.player)
      .subscribe(brokerInfo => {
        this.brokerInfo = brokerInfo.BrokerInfos;
      }, error => {
        this.toastr.warning("Error in loading portfolio", "Warning");
      });
  }
}
