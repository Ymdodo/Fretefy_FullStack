import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './modules/home/home.component';
import { RegiaoComponent } from './modules/regiao/regiao.component';
import { CidadeComponent } from './modules/cidade/cidade.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'regiao',
    component: RegiaoComponent
  },
  {
    path: 'regiao',
    loadChildren: () => import('./modules/regiao/regiao.module').then(m => m.RegiaoModule)
  },
  {
    path: 'cidade',
    component: CidadeComponent
  },
  {
    path: 'cidade',
    loadChildren: () => import('./modules/cidade/cidade.module').then(m => m.CidadeModule)
  },
  {
    path: '**',
    redirectTo: '/home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
