import { LogoutComponent } from "./logout.component";
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LogoutRoutingModule } from './logout-routing.module';

@NgModule({
    imports: [
        CommonModule,
        LogoutRoutingModule
    ],
    declarations: [LogoutComponent]
})

export class LogoutModule { }