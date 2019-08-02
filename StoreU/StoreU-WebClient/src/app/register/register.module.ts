import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { RegisterRoutingModule } from './register-routing.module';
import { RegisterComponent } from './register.component';
import { SharedModule } from '../shared/shared.module';
import { SharedMaterialModule } from '../shared/shared.material.module';
import { CompanyInformationComponent } from './company-information/company-information.component';
import { CompanyBankComponent } from './company-bank/company-bank.component';
import { UserInformationComponent } from './user-information/user-information.component';
import { UserBankComponent } from './user-bank/user-bank.component';


@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        SharedMaterialModule,
        RegisterRoutingModule,
    ],
    declarations: [RegisterComponent, CompanyInformationComponent, CompanyBankComponent, UserInformationComponent, UserBankComponent]
    
})

export class RegisterModule { }