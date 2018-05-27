import { BaseService } from './../core/services/base.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class PlayerService extends BaseService {

  private bankEndPoint = this.baseApiEndPoint + "v1/bank"

  constructor(private http: HttpClient) {
    super();
  }

  createPlayer(player) {
    return this.http.post(`${this.bankEndPoint}/createaccount`,
      player, this.httpOptions)
      .map(response => response)
      .catch(this.errorHandler)
  }


}
