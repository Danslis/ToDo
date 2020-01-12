import { Component, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Card } from '../../model/card';
import { IKeyValue } from "../../interface/iKeyValue";

@Component({
  selector: 'add-dialog',
  templateUrl: './add-dialog.component.html',
  styleUrls: ['./add-dialog.component.css'],
})
export class AddDialogComponent {
  cardModel: Card;
  addForm: FormGroup;
  timeOfCompletionArr: IKeyValue[] = [
    { id: 1, value: "Сегодня" },
    { id: 2, value: "Завтра" },
    { id: 3, value: "Неделя" },
    { id: 4, value: "Месяц" }
  ];
  priorityArr: IKeyValue[] = [
    { id: 1, value: "Низкий" },
    { id: 2, value: "Средний" },
    { id: 3, value: "Высокий" }
  ];
  constructor(private dialogRef: MatDialogRef<AddDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit(): void {
    this.addForm = new FormGroup({
      "name": new FormControl((null), [Validators.required, Validators.maxLength(80)]),
      "timeOfCompletion": new FormControl((null), [Validators.required]),
      "priority": new FormControl((null), [Validators.required]),
    });
  }

  onApplyClick(): void {
    console.log(this.addForm.value);
    this.cardModel = new Card(this.addForm.value);
    this.dialogRef.close(this.cardModel);
  }

  onCancelClick(): void {
    this.dialogRef.close(null);
  }
}
