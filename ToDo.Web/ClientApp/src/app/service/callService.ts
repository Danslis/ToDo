import { Observable } from 'rxjs';
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Card } from '../model/card';


@Injectable()
export class CallService {
private subject = new Subject<any>();

  sendClickCall(card: Card) {
    this.subject.next({ card: card });
}
getClickCall(): Observable < any > {
  return this.subject.asObservable();
}
}
