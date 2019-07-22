import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './shared/guard/auth.guard';
import { LoaderComponent } from './loader/loader.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: './layout/layout.module#LayoutModule',
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    loadChildren: './login/login.module#LoginModule'
  },
  {
    path: 'logout',
    loadChildren: './logout/logout.module#LogoutModule'
  },
  {
    path: 'forgot',
    loadChildren: './forgot/forgot.module#ForgotModule'
  },
  {
    path: 'register',
    loadChildren: './register/register.module#RegisterModule'
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
