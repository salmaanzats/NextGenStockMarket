import { BaseService } from './../core/services/base.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class GameService extends BaseService {

  private stockEndPoint = this.baseApiEndPoint + "v1/stock";
  private bankEndPoint =this.baseApiEndPoint + "v1/bank";

  constructor(private http: HttpClient) {
    super();
  }

  getStockData() {
    return this.http.get(`${this.stockEndPoint}`, this.httpOptions)
      .map(response => response)
      .catch(this.errorHandler)
  }

  getPlayerData(playerName){
    return this.http.get(`${this.bankEndPoint}/bankbalance?playerName=${playerName}`, this.httpOptions)
      .map(response => response)
      .catch(this.errorHandler)
  }

}
