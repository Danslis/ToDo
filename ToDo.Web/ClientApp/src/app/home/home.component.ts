import { Component, Input } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Card } from '../model/card';
import { CardService } from '../service/cardService';
import { CardCollection } from '../model/cardCollection';
import { DeleteDialogComponent } from './dialog-modal/delete-modal/delete-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { CallService } from '../service/callService';
import { UpdateDialogComponent} from './dialog-modal/update-modal/update-dialog.component';
import { SpinnerService } from '../service/spinnerService';
import { IKeyValue } from "../interface/iKeyValue";

/**
 * @title Drag&Drop connected sorting
 */
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  priorityArr: IKeyValue[] = [
    { id: 1, value: "Низкий" },
    { id: 2, value: "Средний" },
    { id: 3, value: "Высокий" }
  ];

  constructor(private service: CardService, private dialog: MatDialog, private callService: CallService,private spiner: SpinnerService) {
    this.loadCard();
  }
  ngOnInit(): void {
    this.callService.getClickCall().subscribe(data => {
      this.loadCard();
    });
  }

  loadCard() {
    this.spiner.activate();
    this.service.getCards().subscribe((data: CardCollection) => {
      this.todoDuringTheToday = data.duringTheToday;
      this.todoDuringTheTomorrow = data.duringTheTomorrow;
      this.todoDuringTheWeek = data.duringTheWeek;
      this.todoDuringTheMonth = data.duringTheMonth;
      this.todoDone = data.done;
      this.spiner.deactivate();
    }, error => {
      console.log(error);
      this.spiner.deactivate();
    });
  }

  getPriority(value: number): string {
    const result: string = this.priorityArr.filter(x => x.id === value).map(x => x.value)[0];
    return result;
  }

  onAddNewCard(data: any) {
    this.loadCard();
  }

  todoDuringTheToday: Card[] = [];
  todoDuringTheTomorrow: Card[] = [];
  todoDuringTheWeek: Card[] = [];
  todoDuringTheMonth: Card[] = [];
  todoDone: Card[] = [];

  submitDone(card: Card) {
    let previousTimeOfCompletion:number = card.timeOfCompletion;
    card.timeOfCompletion = 5;
    this.service.submitDone(card, previousTimeOfCompletion).subscribe((model: Card) => {
      this.loadCard();
    }, error => {
      console.log(error);
    });
  }

  editCard(card: Card) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      updateModelData: card
    };
    dialogConfig.width = '1000px';
    dialogConfig.height = '300px';
    dialogConfig.disableClose = true;
    const dialogRef = this.dialog.open(UpdateDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((cardModel: Card) => {
      if (cardModel !== null) {
        this.service.updateCard(cardModel).subscribe((model: Card) => {
          this.loadCard();
          
        }, error => {
          console.log(error);
        });
      }
    },
      error => {
        console.log(error);
      });
  }

  deleteCard(id: number, timeOfCompletion: number, name: string): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
        name: name
      };
    dialogConfig.width = '350px';
    dialogConfig.disableClose = true;
    const dialogRef = this.dialog.open(DeleteDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(isDelete => {
        if (isDelete) {
          this.service.deleteCard(id).subscribe(() => {
            this.loadCard();
          }, error => {
            console.log(error);
          });
        }
      });
    }   

  

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
      if (event.previousIndex === event.currentIndex) {
        return;
      }
      else {
        let card: Card = JSON.parse(JSON.stringify(event.container.data[event.currentIndex]));
        this.service.moveCard(card, event.currentIndex).subscribe((model: Card) => {
          this.loadCard();
        }, error => {
          console.log(error);
        });
      }
      
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
      let previousTimeOfCompletion: number = +event.previousContainer.id;
      let card: Card = JSON.parse(JSON.stringify(event.container.data[event.currentIndex]));
      card.timeOfCompletion = +event.container.id;
      this.service.moveCardToAnotherBoard(card, event.previousIndex, event.currentIndex, previousTimeOfCompletion).subscribe((model: Card) => {
        this.loadCard();
      }, error => {
        console.log(error);
      });
    }
  }
}
