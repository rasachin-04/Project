import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-car-models',
  templateUrl: './car-models.component.html',
  styleUrls: ['./car-models.component.css']
})
export class CarModelsComponent implements OnInit {
  carModelForm!: FormGroup;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    // Initialize the form with validations
    this.carModelForm = this.formBuilder.group({
      brand: ['', Validators.required],
      class: ['', Validators.required],
      modelName: ['', Validators.required],
      modelCode: ['', [Validators.required, Validators.maxLength(10)]]
    });
  }

  // Function to handle form submission
  onSubmit() {
    if (this.carModelForm.valid) {
      console.log(this.carModelForm.value);
    }
  }
}
