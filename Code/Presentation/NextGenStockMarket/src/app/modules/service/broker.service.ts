import { Injectable } from '@angular/core';
import { BaseService } from '../core/services/base.service';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class BrokerService extends BaseService {

  private brokerEndPoint = this.baseApiEndPoint + "v1/broker";
  
  constructor(private http: HttpClient) {
    super();
  }

  getBrokerData(playerName){
    return this.http.get(`${this.brokerEndPoint}/portfolio?playerName=${playerName}`, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }

  GetAvailableStocks(playerName){
    return this.http.get(`${this.brokerEndPoint}/getavailablestock?playerName=${playerName}`, this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }

  SellStocks(stocks){
    return this.http.post(`${this.brokerEndPoint}/sell`,(stocks), this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }
 
  buyStocks(stocks){
    return this.http.post(`${this.brokerEndPoint}/buy`,(stocks), this.httpOptions)
    .map(response => response)
    .catch(this.errorHandler)
  }
  
}
