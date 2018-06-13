import { GameService } from './../service/game.service';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})

export class GameComponent implements OnInit {

  totalStock = [];
  google = [];
  yahoo = [];
  amazon = [];
  microsoft = [];
  player: string;
  balance:number;

  constructor(private fb: FormBuilder, private router: Router, private gameService: GameService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.player = params['player'];
      if (this.player != null || this.player != undefined) {
        this.loadPlayerData();
        this.loadStockData();
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
}
