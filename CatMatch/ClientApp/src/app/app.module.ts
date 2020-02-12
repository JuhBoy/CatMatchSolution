import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';
import { AppComponent } from './app.component';
import { TileViewComponent } from './components/tile-view/tile-view.component';
import { CatService } from './core/services/cat.service';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { StoreModule } from '@ngrx/store';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { reducer } from './core/tile-view-store/reducer/tile.state.reducer';
import { EffectsModule } from '@ngrx/effects';
import { TileViewEffects } from './core/tile-view-store/effects/tile-view.effects';
import { tileViewKey } from './core/tile-view-store/selectors/tile-view.selector';
import { HomeComponent } from './components/Home/home.component';
import { MatchViewComponent } from './components/match-view/match-view.component';
import { MatchService } from './core/services/match.service';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TileViewComponent,
    HomeComponent,
    MatchViewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'tile-view', component: TileViewComponent, pathMatch: 'full' },
      { path: 'match', component: MatchViewComponent, pathMatch: 'full' }
    ]),
    StoreModule.forRoot({}),
    StoreModule.forFeature(tileViewKey, reducer),
    EffectsModule.forRoot([TileViewEffects]),
    StoreRouterConnectingModule.forRoot(),
    StoreDevtoolsModule.instrument({
      maxAge: 50,
      logOnly: environment.production,
    }),
  ],
  providers: [CatService, MatchService, TileViewEffects],
  bootstrap: [AppComponent]
})
export class AppModule { }
