import { NgModule } from '@angular/core';
//import { CommonModule } from '@angular/common'; 
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CidadeComponent } from './cidade.component';

const routes: Routes = [
    { path: '', component: CidadeComponent }
];

@NgModule({
    declarations: [CidadeComponent],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(routes)
    ]
})
export class CidadeModule { }
