import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { PlayerService } from '../service/player.service';
import { Player } from '../model/player';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  playerForm: FormGroup;
  player = new Player();
  isFormSubmitted = false;

  constructor(private fb: FormBuilder, private router: Router, private playerService: PlayerService,
    private toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.playerForm = this.fb.group({
      playerName: ['', [Validators.required]],
    });
  }

  createPlayer() {
    this.isFormSubmitted = true;
    if (this.playerForm.invalid) return;

    this.player = Object.assign({}, this.player, this.playerForm.value);

    this.playerService.createPlayer(this.player)
      .subscribe(playerInfo => {
        this.router.navigate(['/game',playerInfo.PlayerName]);
        this.toastr.success("Player Has been successfully created!", "Success");
      }, error => {
        this.toastr.error("Player exist with the provided name!try a different name", "Warning");
      })
  }

}
