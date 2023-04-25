package ben.casillas.itemService;

import java.util.*;
import org.springframework.data.mongodb.repository.MongoRepository;


public interface ItemRepository extends MongoRepository<Item, UUID> {
    public List<Item> findByTitleContainingOrDescriptionContaining(String txt, String txt2);


}
