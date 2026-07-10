import { Component, signal } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { LoadingService } from '@services/loading/loading.service';
import { filter } from 'rxjs';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('AuditMed');
  constructor(
    private readonly loading: LoadingService,
    private readonly router: Router,
  ) {
  }
  private readonly minLoadTime = 1500;
  private loadStartTime = 0;
  ngOnInit(): void {
    this.showLoader();
    this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe(() => {
      this.hideLoader();
    });
  }
  private showLoader() {
    this.loading.show();
    this.loadStartTime = Date.now();
  }
  private hideLoader(forceImmediate = false) {
    const elapsedTime = Date.now() - this.loadStartTime;
    const remainingTime = this.minLoadTime - elapsedTime;
    if (remainingTime > 0 && !forceImmediate) {
      setTimeout(() => {
        this.loading.hide();
      }, remainingTime);
    } else {
      this.loading.hide();
    }
  }
}
