import { Component, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Card } from '../../../model/card';
import { IKeyValue } from "../../../interface/iKeyValue";


@Component({
  selector: 'update-dialog',
  templateUrl: './update-dialog.component.html',
  styleUrls: ['./update-dialog.component.css'],
})
export class UpdateDialogComponent {
  cardModel: Card;
  updateForm: FormGroup;
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
  constructor(private dialogRef: MatDialogRef<UpdateDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {
    this.cardModel = data.updateModelData;
  }

  ngOnInit(): void {
    this.updateForm = new FormGroup({
      "name": new FormControl((this.cardModel === null ? null : this.cardModel.name),
        [Validators.required, Validators.maxLength(80)]),
      "timeOfCompletion": new FormControl((this.cardModel === null  ? null : this.cardModel.timeOfCompletion),
        [Validators.required]),
      "priority": new FormControl((this.cardModel === null ? null : this.cardModel.priority), [Validators.required]),
      "id": new FormControl((this.cardModel === null ? null : this.cardModel.id), []),
      "date": new FormControl((this.cardModel === null ? null : this.cardModel.date), []),
    });
  }

  onApplyClick(): void {
    console.log(this.updateForm.value);
    this.cardModel = new Card(this.updateForm.value);
    this.dialogRef.close(this.cardModel);
  }

  onCancelClick(): void {
    this.dialogRef.close(null);
  }
}
