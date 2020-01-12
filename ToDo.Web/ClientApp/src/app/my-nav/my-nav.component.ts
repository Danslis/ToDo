import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { AddDialogComponent } from './add/add-dialog.component';
import { Card } from '../model/card';
import { CardService } from '../service/cardService';
import { CallService } from '../service/callService';


@Component({
  selector: 'my-nav',
  templateUrl: './my-nav.component.html',
  styleUrls: ['./my-nav.component.css']
})
export class MyNavComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );

  constructor(private breakpointObserver: BreakpointObserver, private dialog: MatDialog, private service: CardService, private callService: CallService ) {
  }

  addCard() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '1000px';
    dialogConfig.height = '300px';
    dialogConfig.disableClose = true;
    const dialogRef = this.dialog.open(AddDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((cardModel: Card) => {
      if (cardModel !== null) {
        this.service.addNewCard(cardModel).subscribe((model: Card) => {
          if (model === null) {
            console.log("Ошибка при создании карточки");
          }
          else {
            this.callService.sendClickCall(model);
          }}, error => {
          console.log(error);
          });
      }},
      error => {
        console.log(error);
      });
  }
}
  
  
  


