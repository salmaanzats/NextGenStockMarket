import { BaseService } from './../core/services/base.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class GameService extends BaseService {
 
  private stockEndPoint = this.baseApiEndPoint + "v1/market";
  private bankEndPoint =this.baseApiEndPoint + "v1/bank";
  private brokerEndPoint = this.baseApiEndPoint + "v1/broker";
  private gameEndPoint = this.baseApiEndPoint + "v1/game";

  constructor(private http: HttpClient) {
    super();
  }

  getStockData() {
    return this.http.get(`${this.stockEndPoint}/stock`, this.httpOptions)
      .map(response => response)
      .catch(this.errorHandler)
  }

  getPlayerData(playerName){
    return this.http.get(`${this.bankEndPoint}/bankbalance?playerName=${playerName}`, this.httpOptions)
      .map(response => response)
      .catch(this.errorHandler)
  }

  getSectorData(){
    return this.http.get(`${this.brokerEndPoint}/getsectors`, this.httpOptions)
      .map(response => response)
      .catch(this.errorHandler)
  }

  getStockToSectors(selectedSector){
    return this.http.get(`${this.brokerEndPoint}/getstocks?companyName=${selectedSector}`, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }

  getBankData(playerName){
    return this.http.get(`${this.bankEndPoint}/getaccount?playerName=${playerName}`, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }

  getConnectedPlayers(){
    return this.http.get(`${this.gameEndPoint}/connectedplayers`, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }

  getGameStatus(){
    return this.http.get(`${this.gameEndPoint}/gamestatus`, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }

  getWinner(){
    return this.http.get(`${this.gameEndPoint}/getwinner`, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }

  newGame(){
    return this.http.get(`${this.gameEndPoint}/newgame`, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }
}
