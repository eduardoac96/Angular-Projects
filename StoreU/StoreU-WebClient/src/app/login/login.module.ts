import { CommonModule } from "@angular/common";
import { SharedModule } from '../shared/shared.module';
import { SharedMaterialModule } from '../shared/shared.material.module';
import { LoginComponent } from './login.component';
import { NgModule } from '@angular/core';
import { LoginRoutingModule } from './logout-routing.module'; 

@NgModule({
    imports: [
        CommonModule,
        SharedModule, 
        SharedMaterialModule,
        LoginRoutingModule
    ],
    declarations: [LoginComponent]

})

export class LoginModule{}