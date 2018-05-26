import { GameService } from './../service/game.service';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  constructor(private fb: FormBuilder, private router: Router, private gameService: GameService, private activatedRoute: ActivatedRoute,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.loadStockData();
  }

  loadStockData() {
    this.gameService.getStockData()
      .subscribe(data => {
        debugger
        console.log(data);
      }, error => {
        this.toastr.error("Stock data loading error", "Warning");
      })
  }

}
