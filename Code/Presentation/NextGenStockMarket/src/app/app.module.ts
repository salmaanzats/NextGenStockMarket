import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { PlayerComponent } from './modules/player/player.component';
import { CoreModule } from './modules/core/core.module';
import { SharedModule } from './modules/shared/shared.module';
import { PlayerService } from './modules/service/player.service';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GameComponent } from './modules/game/game.component';
import { GameService } from './modules/service/game.service';
import { SidebarComponent } from './modules/layout/sidebar/sidebar/sidebar.component';
import { BankTransactionComponent } from './modules/bank-transaction/bank-transaction.component';
import { PlayerPortfolioComponent } from './modules/player-portfolio/player-portfolio.component';


@NgModule({
  declarations: [
    AppComponent,
    PlayerComponent,
    GameComponent,
    SidebarComponent,
    BankTransactionComponent,
    PlayerPortfolioComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    CoreModule.forRoot(),
  ],
  providers: [PlayerService, GameService],
  bootstrap: [AppComponent]
})
export class AppModule { }
