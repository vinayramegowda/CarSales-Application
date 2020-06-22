import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public cars: Car[];

    constructor(private readonly router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Car[]>(baseUrl + 'api/cars').subscribe(result => {
        this.cars = result;
      },
        error => console.error(error));
  }
}

interface Car {
  carBodyType: string;
  numberOfDoors: number;
  numberOfWheels: number;
  vehicleType: string;
  make: string;
  model: string;
  engine: string;
  id: number;
}
