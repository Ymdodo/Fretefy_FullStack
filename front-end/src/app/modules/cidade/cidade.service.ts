import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Cidade {
    id?: number;
    nome: string;
    uf: string;
}

@Injectable({
    providedIn: 'root'
})
export class CidadeService {
    private apiUrl = 'https://localhost:44397/api/cidade'; 

    constructor(private http: HttpClient) { }


    getCidades(): Observable<Cidade[]> {
        return this.http.get<Cidade[]>(this.apiUrl);
    }

    getCidadeById(id: number): Observable<Cidade> {
        return this.http.get<Cidade>(`${this.apiUrl}/${id}`);
    }

    createCidade(cidade: Cidade): Observable<any> {
        return this.http.post(this.apiUrl, cidade);
    }

    updateCidade(id: number, cidade: Cidade): Observable<any> {
        return this.http.put(`${this.apiUrl}/${id}`, cidade);
    }

    deleteCidade(id: number): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
}
