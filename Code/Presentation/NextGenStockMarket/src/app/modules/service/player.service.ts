import { BaseService } from './../core/services/base.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class PlayerService extends BaseService {

  private bankEndPoint = this.baseApiEndPoint + "v1/bank";
  private brokerEndPoint = this.baseApiEndPoint + "v1/broker";

  constructor(private http: HttpClient) {
    super();
  }

  createPlayer(player) {
    return this.http.post(`${this.bankEndPoint}/createaccount`,
      player, this.httpOptions)
      .map(response => response)
      .catch(this.errorHandler)
  }

  checkExistingPlayer(player){
    return this.http.post(`${this.bankEndPoint}/getaccount`,
    player, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }


  createBrokerAccount(playerName){
    return this.http.post(`${this.brokerEndPoint}/createaccount?playerName=${playerName}`,playerName, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }
}
