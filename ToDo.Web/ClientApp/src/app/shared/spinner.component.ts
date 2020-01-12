import { Component, Input, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerService } from '../service/spinnerService';


@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent implements OnInit {
  @Input() show: boolean;

  constructor(private spinner: NgxSpinnerService, private spinnerService: SpinnerService) { }

  ngOnInit() {

    this.spinnerService.getData().subscribe(data => {
      if (data) {
        this.spinner.show('componentSpiner');
      } else {
        this.spinner.hide('componentSpiner');
      }
    });
  }
}
