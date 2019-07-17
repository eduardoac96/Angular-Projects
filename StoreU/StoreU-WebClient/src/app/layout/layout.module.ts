import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { LayoutRoutingModule } from './layout-routing.module';
import { LayoutComponent } from './layout.component';
import { LeftMenuComponent } from './left-menu/left-menu.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { AboutComponent } from '../components/about/about.component';


@NgModule({
    imports: [
        CommonModule,
        LayoutRoutingModule
    ],
    declarations: [LayoutComponent, LeftMenuComponent, TopMenuComponent,AboutComponent]

})

export class LayoutModule { }