import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarModelsComponent } from './car-models/car-models.component';

const routes: Routes = [
  { path: 'car-models', component: CarModelsComponent },
  { path: '', redirectTo: '/car-models', pathMatch: 'full' }, // Default route
  { path: '**', redirectTo: '/car-models' } // Redirect unknown URLs
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
