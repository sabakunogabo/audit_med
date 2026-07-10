import { Routes } from '@angular/router';

export const COMPONENT_ROUTES: Routes = [
  {
    path: '',
    title: 'Principal',
    loadComponent: () =>
      import('./atencion-main-view/atencion-main-view.component').then(
        (m) => m.AtencionMainViewComponent,
      ),
      data: {
      routePath: '',
    },
  },
];