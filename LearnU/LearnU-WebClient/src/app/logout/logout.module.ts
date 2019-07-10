import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule, MatCheckboxModule, MatInputModule } from '@angular/material';
import { LogoutComponent } from './logout.component';
import { LogoutRoutingModule } from './logout-routing.module';
 

@NgModule({
    imports: [
        CommonModule,
        LogoutRoutingModule,
        MatInputModule,
        MatCheckboxModule,
        MatButtonModule,
        FlexLayoutModule.withConfig({addFlexToParent: false})
    ],
    declarations: [LogoutComponent]
})
export class LogoutModule {}
