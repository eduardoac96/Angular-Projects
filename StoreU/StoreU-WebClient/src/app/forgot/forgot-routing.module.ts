import { ForgotComponent } from "./forgot.component";
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForgotResetComponent } from './forgot-reset/forgot-reset.component';


const routes: Routes = [
    {

        path: '',
        component: ForgotComponent,
    },
    {
        path: 'reset/:id',
        component: ForgotResetComponent
    }

];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class ForgotRoutingModule { }