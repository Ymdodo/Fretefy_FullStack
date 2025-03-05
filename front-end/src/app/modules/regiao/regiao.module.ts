import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { RegiaoRoutingModule } from './regiao-routing.module';
import { RegiaoComponent } from './regiao.component';
import { CidadeSelectorComponent } from './cidade-selector.component';

@NgModule({
  declarations: [
    RegiaoComponent,
    CidadeSelectorComponent 
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RegiaoRoutingModule
  ]
})
export class RegiaoModule { }
