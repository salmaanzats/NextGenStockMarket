import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class BlockUiService {

  private blockUI = new Subject<boolean>();
  start$ = this.blockUI.asObservable();
  
  constructor() { }

  BlockUI(message: boolean) {
      this.blockUI.next(message);
  }
}
