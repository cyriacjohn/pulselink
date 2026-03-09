import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Dashboard } from './features/dashboard/dashboard';
import { AuthGuard } from './core/guards/auth.guard';
import { Donors } from './features/donors/donors';
import { MainLayout } from './features/main-layout/main-layout';
import { DonorForm } from './features/donors/donor-form/donor-form';
import { ThankYou } from './features/thank-you/thank-you';
import { Donations } from './features/donations/donations';
import { Inventory } from './features/inventory/inventory';
import { Register } from './features/auth/register/register';
import { Donate } from './features/donate/donate';
import { MyDonations } from './features/my-donations/my-donations';
import { Certificates } from './features/certificates/certificates';


export const routes: Routes = [
  {
    path: '', component: MainLayout, canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: Dashboard},
      { path: 'donors', component: Donors },
      {
        path: 'donors/create', component: DonorForm
      },
      {
        path: 'donors/edit/:id', component: DonorForm
      },
      {
        path: 'thank-you', component: ThankYou
      },
      {
        path: 'donations', component: Donations
      },
      {
        path: 'inventory', component: Inventory
      },
      {
        path: 'donate', component: Donate
      },
      {
        path: 'my-donations', component: MyDonations
      },
      {
        path: 'certificates', component: Certificates
      }
    ]
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register}
  
];
