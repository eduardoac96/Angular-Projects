import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import {
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatSidenavModule,
    MatToolbarModule
} from '@angular/material'; 
import { LayoutRoutingModule } from './layout-routing.module'; 
import { LayoutComponent } from './layout.component';
import { NavComponent } from './components/nav/nav.component';
import { TopnavComponent } from './components/topnav/topnav.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { TranslateModule } from '@ngx-translate/core';
import { GlobalErrorHandler } from '../shared/helpers/global-error-handler';

@NgModule({
    imports: [
        CommonModule,
        LayoutRoutingModule,
        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatIconModule,
        MatInputModule,
        MatMenuModule,
        MatListModule,
        TranslateModule
    ],
    declarations: [LayoutComponent, NavComponent, TopnavComponent, SidebarComponent],
    providers: [GlobalErrorHandler]

})
export class LayoutModule { }
