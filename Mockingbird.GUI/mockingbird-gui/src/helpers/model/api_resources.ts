import type { Method } from './method';

export interface ApiResource {
    api_resource_id: string;
    name: string;
    url: string;
    methods?: Method[];

}