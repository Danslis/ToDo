import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Card } from '../model/card';
import { CardCollection } from '../model/cardCollection';


@Injectable()
export class CardService {
  baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getCards(): Observable<CardCollection> {
    return this.http.get<CardCollection>(`${this.baseUrl}api/card/getAllCards`);
  }

  public addNewCard(model: Card) {
    return this.http.post(`${this.baseUrl}api/card/addCard`, model);
  }
  public updateCard(model: Card) {
    return this.http.put(`${this.baseUrl}api/card/editCard/${model.id}`, model);
  }
  public submitDone(model: Card, previousTimeOfCompletion: number) {
    return this.http.put(`${this.baseUrl}api/card/submitDone/${previousTimeOfCompletion}`, model);
  }

  public deleteCard(id: number) {
    return this.http.delete(`${ this.baseUrl }api/card/deleteCard/${id}`);
  }

  public moveCard(model: Card, currentBoardPlaceId: number) {
    return this.http.put(`${this.baseUrl}api/card/moveCard/${currentBoardPlaceId}`, model);
  }
  public moveCardToAnotherBoard(model: Card, previousBoardPlaceId: number, currentBoardPlaceId: number, previousTimeOfCompletion: number) {
    return this.http.put(`${this.baseUrl}api/card/moveCardToAnotherBoard/${previousBoardPlaceId}/${currentBoardPlaceId}/${previousTimeOfCompletion}`, model);
  }
}
