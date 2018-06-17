import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { PlayerService } from '../service/player.service';
import { Player } from '../model/player';
import { BrokerService } from '../service/broker.service';
import { mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  playerName: string;
  existingPlayerName: string;
  isFormSubmitted = false;
  player = new Player();

  constructor(private router: Router, private playerService: PlayerService,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
  }

  createPlayer() {
    this.isFormSubmitted = true;
    if (this.playerName == "") return;
    this.player.PlayerName = this.playerName;
    this.playerService.createPlayer(this.player)
      .subscribe(playerInfo => {
        this.playerService.createBrokerAccount(this.player.PlayerName)
          .subscribe(broker => {
            this.router.navigate(['/game', playerInfo.PlayerName]);
            this.toastr.success("Player Has been successfully created!", "Success");
          });
      }, error => {
        this.toastr.warning(error, "warning");
      })
  }

  checkExistingPlayer() {
    this.isFormSubmitted = true;
    if (this.existingPlayerName == "") return;
    this.player.PlayerName = this.existingPlayerName;
    this.playerService.checkExistingPlayer(this.player)
      .subscribe(playerInfo => {
        this.router.navigate(['/game', playerInfo.Accounts.PlayerName]);
      }, error => {
        this.toastr.warning(error, "warning");
      })
  }

}
