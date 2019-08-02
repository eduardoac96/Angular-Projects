import { RegisterComponent } from "./register.component";
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyInformationComponent } from './company-information/company-information.component';
import { CompanyBankComponent } from './company-bank/company-bank.component';
import { UserInformationComponent } from './user-information/user-information.component';
import { UserBankComponent } from './user-bank/user-bank.component';


const routes: Routes = [

    {
        path: '',
        component: RegisterComponent
    },
    {
        path: 'company-information',
        component: CompanyInformationComponent
    },
    {
        path: 'company-bank',
        component: CompanyBankComponent
    },
    {
        path: 'user-information',
        component: UserInformationComponent
    },
    {
        path: 'user-bank',
        component: UserBankComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class RegisterRoutingModule {}