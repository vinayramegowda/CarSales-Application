import { Component, OnInit, Input } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html'
})
export class AddCarComponent implements OnInit {
  @Input() vehicleForm: NgForm;

  constructor() { }

  ngOnInit() {
  }

}
