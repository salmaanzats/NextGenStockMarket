import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { GameService } from '../../../service/game.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  player: string;
  balance: number;
  currentTurn: number = 0; 
  totalTurns:number = 3;

  constructor(private router: Router, private gameService: GameService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.player = params['player'];
      if (this.player != null || this.player != undefined) {
        this.loadPlayerData();
        this.getCurrentTurn();
      }
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

  getCurrentTurn() {
    this.gameService.getBankData(this.player)
      .subscribe(bankdata => {
        this.currentTurn = bankdata.CurrentTurn;
      }, error => {
        this.toastr.warning(error, "Warning");
      });
  }
}
