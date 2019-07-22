import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CommonModule } from '@angular/common';
import { MatGridListModule, MatCardModule, MatTableModule, MatButtonModule, MatIconModule } from '@angular/material';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { StatModule } from 'src/app/shared/modules/stat/stat.module';
import { GlobalErrorHandler } from 'src/app/shared/helpers/global-error-handler';

@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    MatGridListModule,
    StatModule,
    MatCardModule,
    MatTableModule, 
    MatButtonModule,
    MatIconModule,
    
    FlexLayoutModule.withConfig({addFlexToParent: false})

  ],
  providers: [GlobalErrorHandler]
})
export class DashboardModule { }
