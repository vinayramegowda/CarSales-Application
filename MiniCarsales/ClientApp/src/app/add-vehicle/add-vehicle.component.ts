import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Component({
  selector: 'app-add-vehicle',
  templateUrl: './add-vehicle.component.html'
})
export class AddVehicleComponent implements OnInit {
  private readonly http: HttpClient;
    private type: string;
    private route: ActivatedRoute
  vehicleForm: FormGroup;

  constructor(private readonly router: Router,
    route: ActivatedRoute,
    http: HttpClient,
      private fb: FormBuilder) {
    this.route = route;
    this.http = http;
  }

  ngOnInit() {

    this.route.params.subscribe(params => {
      this.type = params['type'];
    });

    this.vehicleForm = this.fb.group({
      make: ['', [Validators.required, Validators.maxLength(50)]],
      model: ['', [Validators.required, Validators.maxLength(50)]],
      engine: ['', [Validators.required, Validators.maxLength(40)]],
      numberOfDoors: ['', [Validators.required, Validators.min(1), Validators.max(10)]],
      numberOfWheels: ['', [Validators.required, Validators.min(3), Validators.max(10)]],
      carBodyType: ['Sedan', Validators.required]
    });

    this.vehicleForm.valueChanges.subscribe(newVal => console.log(newVal));
  }

  onClickSubmit() {
    if (this.vehicleForm.valid) {

      let headers = new HttpHeaders();
      headers.append('Accept', 'application/json');

      let car = {
        vehicleType: 'Car',
        make: this.vehicleForm.value.make,
        model: this.vehicleForm.value.model,
        engine: this.vehicleForm.value.engine,
        carBodyType: this.vehicleForm.value.carBodyType,
        numberOfWheels: this.vehicleForm.value.numberOfWheels,
        numberOfDoors: this.vehicleForm.value.numberOfDoors
      };

      this.http.post('/api/cars', JSON.stringify(car), httpOptions)
        .subscribe(() => this.router.navigateByUrl(''));
    }
  }
}
