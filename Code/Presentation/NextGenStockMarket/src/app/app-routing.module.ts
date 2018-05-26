import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerComponent } from './modules/player/player.component';
import { Routes, RouterModule } from '@angular/router';
import { GameComponent } from './modules/game/game.component';

const routes: Routes = [
  {
    path: '',
    component: PlayerComponent,
  },
  {
    path: 'game/:player',
    component: GameComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
