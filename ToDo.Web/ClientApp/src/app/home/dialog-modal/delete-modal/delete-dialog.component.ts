import { Component, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { MAT_DIALOG_DATA } from "@angular/material/dialog";


@Component({
  selector: 'delete-alert-dialog',
  templateUrl: './delete-dialog.component.html'
})
export class DeleteDialogComponent {
  name: string;

  constructor(
    private dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.name = data.name;
  }

  onOkClick(): void {
    this.dialogRef.close(true);
  }

  onCancelClick(): void {
    this.dialogRef.close(false);
  }

}
