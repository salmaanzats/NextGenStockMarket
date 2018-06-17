import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { GameService } from '../service/game.service';

@Component({
  selector: 'app-bank-transaction',
  templateUrl: './bank-transaction.component.html',
  styleUrls: ['./bank-transaction.component.css']
})
export class BankTransactionComponent implements OnInit {

  player: string;
  bankInfo = [];

  constructor(private router: Router, private gameService: GameService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.player = params['player'];
      if (this.player != null || this.player != undefined) {
        this.loadBankTransactions();
      }
    });
  }

  loadBankTransactions(){
    this.gameService.getBankData(this.player)
      .subscribe(bankInfo => {
        this.bankInfo = bankInfo.BankTransactions;
      }, error => {
        this.toastr.warning("Error in loading bank info", "Warning");
      });
  }

}
