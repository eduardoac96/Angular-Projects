import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatComponent } from './stat.component';
import { MatCardModule, MatGridListModule, MatIconModule } from '@angular/material';

@NgModule({
  imports: [CommonModule, MatCardModule, MatGridListModule, MatIconModule],
  declarations: [StatComponent],
  exports: [StatComponent]
  
})
export class StatModule { }
