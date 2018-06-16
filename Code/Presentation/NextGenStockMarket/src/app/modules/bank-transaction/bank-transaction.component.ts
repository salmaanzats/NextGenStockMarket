import { Component, OnInit , ViewContainerRef} from '@angular/core';
import { GameService } from './../service/game.service';
import { FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-bank-transaction',
  templateUrl: './bank-transaction.component.html',
  styleUrls: ['./bank-transaction.component.css']
})
export class BankTransactionComponent implements OnInit {

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
