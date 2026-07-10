import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./components/component.routes').then((m) => m.COMPONENT_ROUTES),
  },
];
