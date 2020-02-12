import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { CommonResponse } from "../models/common.response.model";
import { Cat } from "../models/cat.model";
import { environment } from "src/environments/environment";
import { retry } from "rxjs/operators";
import { Cats } from "../models/cats.model";

@Injectable()
export class CatService {

    public constructor(private httpClient: HttpClient) { }

    public getAll(): Observable<CommonResponse<Cats>> {
        const path: string = `${environment.api.host}${environment.api.paths.cats.getAll}`;
        return this.httpClient.get<CommonResponse<Cats>>(path).pipe(retry(3));
    }

    public get(id: number): Observable<CommonResponse<Cat>> {
        const path: string = `${environment.api.host}${environment.api.paths.cats.get.replace('{id}', id.toString())}`;
        return this.httpClient.get<CommonResponse<Cat>>(path).pipe(retry(3));
    }
}