import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegiaoComponent } from './regiao.component';

const routes: Routes = [
    { path: 'nova', component: RegiaoComponent },
    { path: ':id', component: RegiaoComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RegiaoRoutingModule { }
