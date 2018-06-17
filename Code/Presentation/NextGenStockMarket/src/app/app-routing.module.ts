import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerComponent } from './modules/player/player.component';
import { Routes, RouterModule } from '@angular/router';
import { GameComponent } from './modules/game/game.component';
import { BankTransactionComponent } from './modules/bank-transaction/bank-transaction.component';
import { PlayerPortfolioComponent } from './modules/player-portfolio/player-portfolio.component';

const routes: Routes = [
  {
    path: '',
    component: PlayerComponent,
  },
  {
    path: 'game/:player',
    component: GameComponent,
  },
  {
    path: 'bank/:player',
    component:BankTransactionComponent,
  },
  {
    path: 'portfolio/:player',
    component:PlayerPortfolioComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
