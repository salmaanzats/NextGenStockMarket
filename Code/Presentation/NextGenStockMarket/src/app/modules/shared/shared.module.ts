import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastOptions } from 'ng2-toastr';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { ToasterCustomOptions } from './toasterCustomOptions';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    ToastModule.forRoot(),
  ],
  declarations: [],
  exports: [
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    ToastModule
  ],
  providers: [
    { provide: ToastOptions, useClass: ToasterCustomOptions }]
})
export class SharedModule { }
